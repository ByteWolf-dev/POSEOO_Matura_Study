import { CruiserShipDto } from '../models/cruiser-ship-dto.model';

export interface ShippingCompanyDto {
    id: number,
    name: string,
    city?: string,
    plz?: string
    street?: string,
    streetNo?: string,
    cruiseShips?: CruiserShipDto[] | null
}
