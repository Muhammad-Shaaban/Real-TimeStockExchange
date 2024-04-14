import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { SignalRService } from 'src/app/_services/signal-r.service';
import { Stock, StockDTO, StockDTODataSourceResult, SwaggerClient } from 'src/app/_services/swagger/SwaggerClient.service';

@Component({
  selector: 'app-get-all-stock',
  templateUrl: './get-all-stock.component.html',
  styleUrls: ['./get-all-stock.component.css']
})
export class GetAllStockComponent implements OnInit {

  public allStocks: Stock[] = [];
  public stockHistory: StockDTO = new StockDTO();
  public StockCount: number = 0;
  public visible: boolean = false;
  public stocklHistoryVisible: boolean = false;
  public isAdmin: boolean = false;
  public stockForm: StockDTO = new StockDTO();

  constructor(
    private service: SwaggerClient,
    private authService: AuthService,
    private signalr: SignalRService) {

  }
  
  ngOnInit(): void {
    this.GetAllStocks();
    this.isAdmin = this.authService.CheckIsAdmin();
    this.TrackStokeUpdates();
  }

  GetAllStocks = () => {
    this.service.apiStockGetAllGet(10, 1).subscribe({
      next: (res: StockDTODataSourceResult) => {
        this.allStocks = res.data!;
        this.StockCount = res.count!;
      },

      error: (err: any) => {
        console.log(err)
      }
    });
  }

  onPageChange = (event: any) => {
    this.service.apiStockGetAllGet(event.rows, (event.page + 1)).subscribe({
      next: (res: StockDTODataSourceResult) => {
        this.allStocks = res.data!;
      },

      error: (err: any) => {
        console.log(err)
      }
    });
  }

  showDialog = () => {
    this.visible = true;
  }

  AddNewSymbol = () => {
    this.service.apiStockInsertPost(this.stockForm).subscribe({
      next: (res: StockDTO) => {
        this.visible = false;
        this.GetAllStocks();

        this.stockForm.symbol = '';
        this.stockForm.currentPrice = '';
      },
      error: (err: any) => {
        console.log(err)
      }
    });
  }

  SymbolHistory = (id: number) => {
    this.stocklHistoryVisible = true;
    this.service.apiStockGetStockHistoryGet(id).subscribe({
      next: (res: StockDTO) => {
        this.stockHistory = res;
      },

      error: (err: any) => {
        console.log(err)
      }
    });
  }

  TrackStokeUpdates = () => {
    this.signalr.on("UpdateStockRealTime", (Id, CurrentPrice, Time) => {
      let stock = this.allStocks.find(s => s.id === Id);
      if(stock){
        stock.currentPrice = CurrentPrice;
        stock.time = Time;
      }

    })
  }
}
