import type { IEntity } from "./IEntity";

export interface IClient extends IEntity {
    name: string,
    pesel: string | null,
    nip: string | null,
    company: boolean,
}
