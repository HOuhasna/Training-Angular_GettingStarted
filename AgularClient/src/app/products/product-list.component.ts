import { Component, OnInit, OnChanges, SimpleChanges, SimpleChange } from "@angular/core";
import { IProduct} from "./product";
import { ProductService } from "./product.service";

@Component({
    selector : 'pm-products',
    templateUrl : './product-list.component.html'
    ,styleUrls : ['./product-list.component.css']

})
export class ProductListComponent implements OnInit, OnChanges {

    pageTitle : string = "Acme Product List";
    imageWidth : number = 40;
    imageMargin : number = 2;
    showImage : boolean = false;
    products : IProduct[];
    filteredProducts : IProduct[];
    
    private _listFilter : string;

    private errorMessage : string;

    public get listFilter() : string {
        return this._listFilter;
    }
    public set listFilter(v : string) {
        this._listFilter = v;
        this.filteredProducts = this.listFilter ? this.performFilter(this.listFilter) : this.products;
    }
    
    performFilter(filter : string) : IProduct[] {
        
        filter = filter.toLocaleLowerCase();
        return this.products.filter((product : IProduct)=> product.productName.toLocaleLowerCase().indexOf(filter) >= 0);
        /**Ok too
         * return this.products.filter((product)=> product.productName.toLowerCase().indexOf(this.listFilter) >= 0);
         */
    }

    toggleImage() : void {
        this.showImage = !this.showImage;
    }

    ngOnInit() : void {
        
        console.log("We are onInit!");
        this.productService.getProducts().subscribe(
            products => {
                this.products = products;
                this.filteredProducts = products;
        }, error => {
                     this.errorMessage = <any>error;
                     console.error(this.errorMessage);
                    }
        )

    }

    ngOnChanges(changes: SimpleChanges): void{
        
        console.log("We are OnChanges");
        this.products = this.products.filter((x)=> x.productName.indexOf(this.listFilter) > 0);
        
    }

    onRatingClicked(message:string) : void{
        console.log("Product list notified by star click");
    }

    constructor(private productService : ProductService){

    }

}