import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
  } from '@angular/common/http';
  import { Observable } from 'rxjs';
   
  export class HeaderInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      // Clone the request to add the new header
      const userToken = localStorage.getItem('userToken');   
      console.log(userToken);
      const clonedRequest = req.clone({ headers: req.headers.append('Authorization', 'Bearer '+userToken) });
  
      // Pass the cloned request instead of the original request to the next handle
      return next.handle(clonedRequest);
    }
  }