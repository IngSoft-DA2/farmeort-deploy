import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { CONFIGURACION } from '../configuracion';
import { Importador, ResultadoImportacion, Salud, Skill, SkillNueva } from './modelos';

@Injectable({ providedIn: 'root' })
export class Api {
  // #region url-de-la-api
  // La URL de la API sale de config.json (ver configuracion.ts), no está escrita a mano.
  private readonly http = inject(HttpClient);
  readonly url = inject(CONFIGURACION).apiUrl;

  salud(): Observable<Salud> {
    return this.http.get<Salud>(`${this.url}/api/salud`);
  }

  listarSkills(): Observable<Skill[]> {
    return this.http.get<Skill[]>(`${this.url}/api/skills`);
  }
  // #endregion

  crearSkill(skill: SkillNueva): Observable<Skill> {
    return this.http.post<Skill>(`${this.url}/api/skills`, skill);
  }

  desactivarSkill(codigo: string): Observable<Skill> {
    return this.http.delete<Skill>(`${this.url}/api/skills/${encodeURIComponent(codigo)}`);
  }

  listarImportadores(): Observable<Importador[]> {
    return this.http.get<Importador[]>(`${this.url}/api/importadores`);
  }

  importar(nombre: string, parametros: Record<string, string>): Observable<ResultadoImportacion> {
    return this.http.post<ResultadoImportacion>(
      `${this.url}/api/importadores/${encodeURIComponent(nombre)}/importaciones`,
      { parametros },
    );
  }
}

// Traduce un error HTTP a un mensaje para mostrar. Status 0 = el navegador no obtuvo respuesta:
// la API no está corriendo, la URL de config.json está mal o CORS bloqueó el pedido (ver la consola).
export function mensajeDeError(error: unknown): string {
  if (error instanceof HttpErrorResponse) {
    if (error.status === 0) {
      return 'No se pudo conectar con la API. Revisá que esté corriendo, la URL de config.json y CORS (consola del navegador).';
    }
    return error.error?.mensaje ?? `Error ${error.status}`;
  }
  return String(error);
}
