import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/_services/notification.service';
import { LookUpDTO, Order, OrderDTO, OrderDTODataSourceResult, SwaggerClient } from 'src/app/_services/swagger/SwaggerClient.service';

@Component({
  selector: 'app-get-all-order-history',
  templateUrl: './get-all-order-history.component.html',
  styleUrls: ['./get-all-order-history.component.css']
})
export class GetAllOrderHistoryComponent implements OnInit {

  public allOrders: OrderDTO[] = [];
  public orderCount: number = 0;
  public visible: boolean = false;
  public orderForm: OrderDTO = new OrderDTO();
  public stockSymbolLookup: LookUpDTO [] = [];
  public OrderTypesLookup: LookUpDTO[] = [];

  constructor(
    private service: SwaggerClient, 
    private tostar: NotificationService) {    
  }
    
  ngOnInit(): void {
    this.GetAllHistoryOrders();
    this.getStockSymbolLookup();
    this.getOrderTypesLookup();
  }

  GetAllHistoryOrders = () => {
    this.service.apiOrderGetAllOrderHistoryForCurrentUserGet(1, 10).subscribe({
      next: (res: OrderDTODataSourceResult) => {
        this.allOrders = res.data!;
        this.orderCount = res.count!;
      },
      error: (err: any) => {
        console.log(err)
      }
    });
  }

  onPageChange = (event: any) => {
    this.service.apiOrderGetAllOrderHistoryForCurrentUserGet(event.rows, (event.page + 1)).subscribe({
      next: (res: OrderDTODataSourceResult) => {
        this.allOrders = res.data!;
      },

      error: (err: any) => {
        console.log(err)
      }
    });
  }

  showDialog = () => {
    this.visible = true;

  }

  getStockSymbolLookup = () => {
    this.service.apiLookupGetLookUpGet('stock').subscribe({
      next: (res: LookUpDTO[]) => {
        this.stockSymbolLookup = res;
      }
    });
  }

  getOrderTypesLookup = () => {
    this.OrderTypesLookup.push(
      new LookUpDTO({id: 1, text: 'Buy'}),
      new LookUpDTO({id: 2, text: 'Sell'})
    );
  }

  OrderNow = () => {
    this.service.apiOrderInsertPost(this.orderForm).subscribe({
      next: (res: OrderDTO) => {
        this.orderForm.orderTypeId = 0;
        this.orderForm.quantity = 0;
        this.orderForm.stockSymbolId = 0;

        this.visible = false;
        this.GetAllHistoryOrders();
        this.tostar.showSuccess('Successful Order');
      },

      error: (err: any) => {
        this.tostar.showError('Error ');
      }
    });
  }
}
