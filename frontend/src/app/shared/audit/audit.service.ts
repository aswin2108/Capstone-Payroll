import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Audit } from '../model/Audit';

@Injectable({
  providedIn: 'root'
})
export class AuditService {

  constructor(private http:HttpClient) { }

  fetchAudit(){
    return this.http.get<Audit[]>('https://localhost:7125/api/Audit/audit', {});
  }
}
