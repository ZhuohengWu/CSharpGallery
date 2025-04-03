import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Product } from '../models/products.model';
import { ProductDto } from '../models/products.dto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = environment.apiUrl; 

  private http = inject(HttpClient);

  getProducts(): Observable<Product[]> {
    return this.http.get<ProductDto[]>(`${this.apiUrl}/products`)
      .pipe(map((data) => data.map(this.mapProductDtoToProduct))
    );
  }

  private mapProductDtoToProduct(productDto: ProductDto): Product {
    return {
      id: productDto.id,
      title: productDto.name,
      description: productDto.description,
      price: productDto.price,
      image: "",
      stock: 0
    };
  }
}
