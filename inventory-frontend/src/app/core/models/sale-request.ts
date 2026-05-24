import { SaleDetailRequest } from './sale-detail-request';

export interface SaleRequest {
    subTotal: number;
    igv: number;
    total: number;
    details: SaleDetailRequest[];
}