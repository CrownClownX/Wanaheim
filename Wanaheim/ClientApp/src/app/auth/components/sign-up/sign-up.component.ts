import { NewUser } from './../../../models/newUser.model';
import { AuthStore } from './../../store/auth.store';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.styl']
})
export class SignUpComponent implements OnInit {
  private form: FormGroup;
  submited = false;

  constructor(private authStore:AuthStore) { }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl("", Validators.required),
      email: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required, Validators.minLength(8)]),
      agreement: new FormControl("", Validators.required)
    });
  }

  get controls(){
    return this.form.controls;
  }

  onSignUp(){
    this.submited = true;

    if(this.form.valid){
      const values = this.form.value;

      const payload:NewUser = {
        name: values.name,
        email: values.email,
        password: values.password
      };

      this.authStore.dispatchSignupAction(payload);
    }
  }


}
