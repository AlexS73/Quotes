import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {IValCursModel} from "../Interfaces/IValCurs.model";

@Injectable()
export class DataService {
  constructor(private http: HttpClient) {}

  public GetValutes(date: Date){
    return this.http.get<IValCursModel>('/api/Data', {
      params: new HttpParams().set('date', date.toISOString())
    })
  }
}
