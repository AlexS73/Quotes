import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {map, tap} from "rxjs/operators";

const defaultPath = '/';

@Injectable()
export class AuthService {

  private user = null;
  private _lastAuthenticatedPath: string = defaultPath;
  private refreshTokenTimeout;

  constructor(private http: HttpClient, private router: Router) {}

  LogIn(UserName: string, Password: string){
    return this.http.post('/api/Account/login', {UserName, Password})
      .pipe(
        tap(this.setToken)
      )
  }

  LogOut(){
    this.http.post<any>(`/api/Account/revoke-token`, {}, { withCredentials: true }).subscribe();
    this.stopRefreshTokenTimer();
    this.setToken(null);
    this.router.navigate(['/login']);
  }

  RefreshToken(){
    return this.http.post<any>(`/api/Account/refresh-token`, null)
      .pipe(map((response) => {
          this.setToken(response);
        })
      );
  }

  get loggedIn(): boolean {
    return !!this.token;
  }

  public get token(): string{
    const expDate = new Date(localStorage.getItem('token-exp'));
    if (new Date() > expDate){
      localStorage.clear();
      return null;
    }
    return localStorage.getItem('token');
  }

  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  private setToken(response){
    if (response){
      const jwtToken = JSON.parse(atob(response.JwtToken.split('.')[1]));
      const expDate = new Date(jwtToken.exp * 1000);
      localStorage.setItem('token', response.JwtToken);
      localStorage.setItem('token-exp', expDate.toString());
      this.startRefreshTokenTimer();
      this.user = { email: response.UserName };
    }
    else {
      this.user = null;
      localStorage.clear();
    }
  }

  private startRefreshTokenTimer(){
    const jwtToken = JSON.parse(atob(this.token.split('.')[1]));

    // set a timeout to refresh the token a minute before it expires
    const expires = new Date(jwtToken.exp * 1000);
    const timeout = expires.getTime() - Date.now() - (60 * 1000);
    this.refreshTokenTimeout = setTimeout(() => this.RefreshToken().subscribe(), timeout);
  }

  private stopRefreshTokenTimer(): void {
    clearTimeout(this.refreshTokenTimeout);
  }

}
