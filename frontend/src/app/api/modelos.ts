export interface Skill {
  id: number;
  codigo: string;
  nombre: string;
  categoria: string;
  activa: boolean;
}

export interface SkillNueva {
  codigo: string;
  nombre: string;
  categoria: string;
}

export interface Parametro {
  nombre: string;
  descripcion: string;
}

export interface Importador {
  nombre: string;
  parametros: Parametro[];
}

export interface ResultadoImportacion {
  importador: string;
  importadas: number;
  rechazadas: { codigo: string; motivo: string }[];
}

export interface Salud {
  estado: string;
  entorno: string;
  baseDeDatosConectada: boolean;
  carpetaPlugins: string;
  importadores: string[];
}
