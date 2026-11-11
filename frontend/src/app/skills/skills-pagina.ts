import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Api, mensajeDeError } from '../api/api';
import { Skill, SkillNueva } from '../api/modelos';

@Component({
  selector: 'app-skills-pagina',
  imports: [FormsModule],
  templateUrl: './skills-pagina.html',
})
export class SkillsPagina implements OnInit {
  private readonly api = inject(Api);
  protected readonly skills = signal<Skill[]>([]);
  protected readonly error = signal<string | null>(null);
  protected readonly ok = signal<string | null>(null);
  protected nueva: SkillNueva = { codigo: '', nombre: '', categoria: '' };

  ngOnInit(): void {
    this.cargar();
  }

  protected cargar(): void {
    this.api.listarSkills().subscribe({
      next: (skills) => this.skills.set(skills),
      error: (error) => this.error.set(mensajeDeError(error)),
    });
  }

  protected crear(): void {
    this.limpiarMensajes();
    this.api.crearSkill(this.nueva).subscribe({
      next: (creada) => {
        this.ok.set(`Skill ${creada.codigo} creada.`);
        this.nueva = { codigo: '', nombre: '', categoria: '' };
        this.cargar();
      },
      error: (error) => this.error.set(mensajeDeError(error)),
    });
  }

  protected desactivar(skill: Skill): void {
    this.limpiarMensajes();
    this.api.desactivarSkill(skill.codigo).subscribe({
      next: () => {
        this.ok.set(`Skill ${skill.codigo} desactivada.`);
        this.cargar();
      },
      error: (error) => this.error.set(mensajeDeError(error)),
    });
  }

  private limpiarMensajes(): void {
    this.error.set(null);
    this.ok.set(null);
  }
}
