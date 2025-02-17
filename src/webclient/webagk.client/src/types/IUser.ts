import type IEntity from "./IEntity";

export default interface IUser extends IEntity {
	name?: string;
	email?: string;
	firstName?: string;
  lastName?: string;
  role?: string;
  permissions?: Record<string, string[]>;
	isConfirmed?: boolean;
}
