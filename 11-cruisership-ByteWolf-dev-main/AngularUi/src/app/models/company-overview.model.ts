import { CruiserShipDto } from "./cruiser-ship-dto.model"

export interface CompanyOverview {
    id: number,
    name: string,
    street: string
    streetNo: string,
    city: string,
    plz: string
    cruiseShips: CruiserShipDto[]
}
