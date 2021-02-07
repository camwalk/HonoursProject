export interface Pagination {
    currentPage: number;
    itemsOnPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T> {
    result: T;
    pagination: Pagination;
}