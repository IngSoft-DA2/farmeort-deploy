import { Component, OnInit, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Api, mensajeDeError } from './api/api';
import { Salud } from './api/modelos';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  protected readonly api = inject(Api);
  protected readonly salud = signal<Salud | null>(null);
  protected readonly errorSalud = signal<string | null>(null);

  ngOnInit(): void {
    this.consultarSalud();
  }

  // La barra de estado muestra a qué API apunta el front y si responde: lo primero a mirar tras un deploy.
  protected consultarSalud(): void {
    this.errorSalud.set(null);
    this.api.salud().subscribe({
      next: (salud) => this.salud.set(salud),
      error: (error) => {
        this.salud.set(null);
        this.errorSalud.set(mensajeDeError(error));
      },
    });
  }
}
