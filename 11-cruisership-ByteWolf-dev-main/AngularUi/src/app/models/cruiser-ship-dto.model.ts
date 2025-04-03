export interface CruiserShipDto {
    id: number,
    name: string,
    yearOfConstruction: number,
    tonnage?: number,
    length?: number,
    cabins?: number,
    passengers?: number,
    crew?: number,
    remark?: string,
    oldNames?: string
}