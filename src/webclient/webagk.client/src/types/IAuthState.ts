import type IUser from "./IUser";

export default interface AuthState extends IUser {
  accessToken: string | null;
  refreshToken: string | null;
  returnUrl?: string;
  isAuthenticated: boolean;
  isRefreshing: boolean,
}
