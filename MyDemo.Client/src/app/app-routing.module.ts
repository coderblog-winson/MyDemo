import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserManagementComponent } from './user-management/user-management.component';
import { FormLayoutTestingComponent } from './form-layout-testing/form-layout-testing.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './core/auth.guard';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { GeneratepdfComponent } from './generatepdf/generatepdf.component';
import { ReportComponent } from './report/report.component';

// const routes: Routes = [
//   { path: 'user-management', component: UserManagementComponent, canActivate: [AuthGuard] },
//   { path: 'form-layout', component: FormLayoutTestingComponent, canActivate: [AuthGuard] },
//   { path: 'login', component: LoginComponent },
// ];

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'user-management',
        component: UserManagementComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'form-layout',
        component: FormLayoutTestingComponent,
        canActivate: [AuthGuard],
      },
      { path: 'login', component: LoginComponent },
    ],
  },
  { path: 'generatePDF', component: GeneratepdfComponent },
  { path: 'report', component: ReportComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
