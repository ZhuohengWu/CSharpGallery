import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { ProductsComponent } from "./products/products.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ProductsComponent, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'swiftshop-angular-app';
}
