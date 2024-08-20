import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppHeaderComponent } from './app-header/app-header.component';
import { AppFooterComponent } from './app-footer/app-footer.component';
import { AppLoginComponent } from './app-login/app-login.component';
import { AppProductComponent } from './app-product/app-product.component';
import { AppUserListComponent } from './app-user-list/app-user-list.component';
import { AppShortUserProfileComponent } from './app-short-user-profile/app-short-user-profile.component';
import { AppMainUserProfileComponent } from './app-main-user-profile/app-main-user-profile.component';
import { AppCommentSectionComponent } from './app-comment-section/app-comment-section.component';
import { AppCommentComponent } from './app-comment/app-comment.component';
import { AppProductInfoComponent } from './app-product-info/app-product-info.component';
import { AppProductDetailsComponent } from './app-product-details/app-product-details.component';
import { AppButtonsComponent } from './app-buttons/app-buttons.component';
import { AppLoginButtonsComponent } from './app-login-buttons/app-login-buttons.component';
import { AppHeaderRouteComponentsComponent } from './app-header-route-components/app-header-route-components.component';
import { AppProductShowcaseComponent } from './app-product-showcase/app-product-showcase.component';
import { AppProductItemComponent } from './app-product-item/app-product-item.component';
import { AppSearchComponent } from './app-search/app-search.component';
import { AppProductFiltersComponent } from './app-product-filters/app-product-filters.component';
import { AppPaginationComponent } from './app-pagination/app-pagination.component';
import { AppProductPricesComponent } from './app-product-prices/app-product-prices.component';
import { AppLastRegisteredUsersComponent } from './app-last-registered-users/app-last-registered-users.component';
import { AppOnlineUsersComponent } from './app-online-users/app-online-users.component';
import { AppContactUsComponent } from './app-contact-us/app-contact-us.component';
import { AppEmailSendComponent } from './app-email-send/app-email-send.component';
import { AppContactInfoComponent } from './app-contact-info/app-contact-info.component';
import { AppShopComponent } from './app-shop/app-shop.component';

@NgModule({
  declarations: [
    AppComponent,
    AppHeaderComponent,
    AppFooterComponent,
    AppLoginComponent,
    AppProductComponent,
    AppUserListComponent,
    AppShortUserProfileComponent,
    AppMainUserProfileComponent,
    AppCommentSectionComponent,
    AppCommentComponent,
    AppProductInfoComponent,
    AppProductDetailsComponent,
    AppButtonsComponent,
    AppLoginButtonsComponent,
    AppHeaderRouteComponentsComponent,
    AppProductShowcaseComponent,
    AppProductItemComponent,
    AppSearchComponent,
    AppProductFiltersComponent,
    AppPaginationComponent,
    AppProductPricesComponent,
    AppLastRegisteredUsersComponent,
    AppOnlineUsersComponent,
    AppContactUsComponent,
    AppEmailSendComponent,
    AppContactInfoComponent,
    AppShopComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
