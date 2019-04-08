import { AuthStore } from './../../store/auth.store';
import { Login } from './../../store/auth.actions';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './../../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { noop } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
 //styleUrls: ['./login.component.styl']
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  submited = false;

  constructor(private authStore:AuthStore) { 

  }

  ngOnInit() {
    this.form = new FormGroup({
      email: new FormControl('cizar@gmail.com', Validators.required),
      password: new FormControl('All2All2!', Validators.required)
    });
  }

  get controls(){
    return this.form.controls;
  }

  onLogin(): void {
    this.submited = true;

    if (this.form.valid) {
      const payload = {email: this.form.value.email,
        password: this.form.value.password};

      this.authStore.dispatchLoginAction(payload);
    }
  }

  setInvalidToForm(){
    this.form.setErrors({
      invalid: true
    });
  }

}
