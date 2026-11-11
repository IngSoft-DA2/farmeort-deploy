import { InjectionToken } from '@angular/core';

// Configuración que se lee en tiempo de ejecución desde /config.json (carpeta public).
// Por qué no environment.ts: lo de environment.ts queda compilado dentro del JavaScript.
// Con config.json se cambia la URL de la API editando un archivo del sitio publicado, sin recompilar
// (útil cuando la URL cambia en cada deploy, como la de un túnel de Cloudflare).
export interface Configuracion {
  apiUrl: string;
}

export const CONFIGURACION = new InjectionToken<Configuracion>('CONFIGURACION');

export async function cargarConfiguracion(): Promise<Configuracion> {
  // cache: 'no-store' para que el navegador no use una versión vieja después de un redeploy.
  const respuesta = await fetch('/config.json', { cache: 'no-store' });
  if (!respuesta.ok) {
    throw new Error(`No se pudo leer /config.json (HTTP ${respuesta.status})`);
  }
  const configuracion = (await respuesta.json()) as Configuracion;
  return { apiUrl: configuracion.apiUrl.replace(/\/+$/, '') };
}
