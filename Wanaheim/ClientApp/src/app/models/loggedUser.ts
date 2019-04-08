import { User } from "./user.model";

export interface LoggedUser {
  user: User;
  token: string;
}