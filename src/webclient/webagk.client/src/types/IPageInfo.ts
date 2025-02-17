import type IEntity from "./IEntity";

export default interface IPageInfo {
    pageNumber: number,
    pageSize: number,
    totalCount: number,
    collection: Array<IEntity>,
}