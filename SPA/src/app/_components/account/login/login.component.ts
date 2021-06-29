import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ILoginModel } from 'src/app/_models/login-model';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  credentials: ILoginModel;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastrService: ToastrService
  ) {
    this.credentials = { username: '', password: '' };
  }

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.credentials).subscribe(
      (resp) => {
        this.router.navigateByUrl('/employee/view');
      },
      (err) => {
        this.toastrService.error(err.error.error);
      }
    );
  }

  get AppVersion(): string {
    return this.accountService.appVersion;
  }
}
