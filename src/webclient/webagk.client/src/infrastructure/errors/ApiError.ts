export class ApiError extends Error {

  code: string = "";
  errors: string[] | undefined;

  constructor(message: string, name: string) {
    super(message);
    this.name = name;
    Object.setPrototypeOf(this, ApiError.prototype);
  }
}
