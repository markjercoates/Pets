import { HttpParams, HttpResponse } from "@angular/common/http";
import { signal } from "@angular/core";
import { PaginatedResult } from "../_models/pagination";

export function setPaginatedResponse<T>(response: HttpResponse<T>, 
    paginatedResultSignal: ReturnType<typeof signal<PaginatedResult<T> | null>>) {
        paginatedResultSignal.set({
            items: response.body as T,
            pagination: JSON.parse(response.headers.get('Pagination')!)
        })
}

