import type IEntity from "./IEntity";
import type IAddress from "./IAddress";

export default interface ICustomer extends IEntity {
    lastName: string,
    firstName?: string,
    secondName?: string,
    pesel?: string;
    nip?: string,
    regon?: string,
    company: boolean,
    birthDate?: Date,
    birthPlace?: string;
    citizenship?: string;
    occupation?: string;
    address?: IAddress;
    hasAdditionalAddress?: boolean;
    additionalAddress?: IAddress;
}
