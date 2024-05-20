import type IEntity from "./IEntity";


export default interface IUser extends IEntity {
	firstname: string | null;
	lastname: string | null;
	userName: string;
	normalizedUserName: string | null;
	email: string | null;
	normalizedEmail: string | null;
	emailConfirmed: boolean;
	passwordHash: string;
	securityStamp: string | null;
	concurrencyStamp: string | null;
	phoneNumber: string | null;
	phoneNumberConfirmed: boolean;
	twoFactorEnabled: boolean;
	lockoutEnd: Date | null;
	lockoutEnabled: boolean;
	accessFailedCount: number;
}
