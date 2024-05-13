import type { IEntity } from "./IEntity";

export interface IPageInfo {
    pageNumber: number,
    pageSize: number,
    totalCount: number,
    collection: Array<IEntity>,
}