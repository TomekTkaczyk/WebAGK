export interface IValidationError {
  field: string;
  code: string;
  message: string;
}

export function isValidationError(obj: any): obj is IValidationError {
    return (
        obj !== null &&
        typeof obj === "object" &&
        typeof obj.field === "string" &&
        typeof obj.code === "string" &&
        typeof obj.message === "string"
      );
}
