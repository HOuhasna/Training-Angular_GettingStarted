import {IProduct} from "./product";
import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse} from "@angular/common/http"
import { Observable, throwError } from "rxjs";
import { catchError, tap} from "rxjs/operators";

@Injectable({
    providedIn : "root"
})

export class ProductService{

    private serviceUrl = "api/products/products.json";

    constructor(private http : HttpClient){

    }

    getProducts() : Observable<IProduct[]>{
        
        console.log("We are on service");
        return this.http.get<IProduct[]>(this.serviceUrl).pipe(
            tap(data => console.log('All Products : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    private handleError(err : HttpErrorResponse) {

        console.error("We are on service handleError");
        let errorMessage = '';
        if(err.error instanceof ErrorEvent){
            errorMessage = `An error occured : ${err.error.message}`;
        } else{
            errorMessage = `Server retured code : ${err.status}, error message is : ${err.message}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
    }
}