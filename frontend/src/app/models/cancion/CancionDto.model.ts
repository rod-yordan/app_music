export class CancionDto {
    id: number = 0;
    nombre: string | null = "";
    artista: string | null = "";
    fechaLanzamiento: string | null = ""; // Usamos string para fechas en formato ISO
    idGeneroCancion: number = 0;
}