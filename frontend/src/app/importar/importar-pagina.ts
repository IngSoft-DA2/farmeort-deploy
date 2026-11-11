import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Api, mensajeDeError } from '../api/api';
import { Importador, ResultadoImportacion } from '../api/modelos';

// El formulario se arma con los parámetros que declara cada importador (GET /api/importadores):
// si se agrega un importador nuevo en la carpeta Plugins, aparece acá sin cambiar el front.
@Component({
  selector: 'app-importar-pagina',
  imports: [FormsModule],
  templateUrl: './importar-pagina.html',
})
export class ImportarPagina implements OnInit {
  private readonly api = inject(Api);
  protected readonly importadores = signal<Importador[]>([]);
  protected readonly seleccionado = signal<Importador | null>(null);
  protected readonly resultado = signal<ResultadoImportacion | null>(null);
  protected readonly error = signal<string | null>(null);
  protected valores: Record<string, string> = {};

  ngOnInit(): void {
    this.cargarImportadores();
  }

  protected cargarImportadores(): void {
    this.api.listarImportadores().subscribe({
      next: (importadores) => this.importadores.set(importadores),
      error: (error) => this.error.set(mensajeDeError(error)),
    });
  }

  protected elegir(nombre: string): void {
    this.seleccionado.set(this.importadores().find((i) => i.nombre === nombre) ?? null);
    this.valores = {};
    this.resultado.set(null);
    this.error.set(null);
  }

  protected importar(): void {
    const importador = this.seleccionado();
    if (!importador) {
      return;
    }
    this.error.set(null);
    this.api.importar(importador.nombre, this.valores).subscribe({
      next: (resultado) => this.resultado.set(resultado),
      error: (error) => this.error.set(mensajeDeError(error)),
    });
  }
}
