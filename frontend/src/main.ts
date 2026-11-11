import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { CONFIGURACION, cargarConfiguracion } from './app/configuracion';

// Primero se lee /config.json y recién después arranca Angular, con la configuración ya disponible.
cargarConfiguracion()
  .then((configuracion) =>
    bootstrapApplication(App, {
      ...appConfig,
      providers: [...appConfig.providers, { provide: CONFIGURACION, useValue: configuracion }],
    }),
  )
  .catch((error) => {
    console.error(error);
    document.body.innerHTML = `<p style="font-family:sans-serif;padding:2rem">No se pudo iniciar la aplicación: ${error}</p>`;
  });
