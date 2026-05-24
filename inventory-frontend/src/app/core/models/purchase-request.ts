import { PurchaseDetailRequest } from './purchase-detail-request';

export interface PurchaseRequest {
  subTotal: number;
  igv: number;
  total: number;
  details: PurchaseDetailRequest[];

}