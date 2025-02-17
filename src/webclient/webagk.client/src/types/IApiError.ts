import {isValidationError,  type IValidationError } from "./IValidationError";

export interface IApiError {
  code: string;
  detail: string;
  instance: string;
  message: string;
  status: number;
  title: string;
  type: string;
  validationErrors: IValidationError[];
}

export function isApiError(obj: any): obj is IApiError {
    return (
        obj !== null &&
        typeof obj === "object" &&
        (typeof obj.code === "string" || obj.code === null) &&
        (typeof obj.message === "string" || obj.message === null) &&
        (typeof obj.status === "number" || obj.status === null) &&
        Array.isArray(obj.validationErrors) &&
        obj.validationErrors.every(
          (error: any) => isValidationError(error)
        )
      );
}
