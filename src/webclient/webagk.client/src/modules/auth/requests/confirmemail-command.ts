export default interface IConfirmEmailCommand {
  email: string,
  confirmToken: string,
}


export function isConfirmEmailCommand(obj: any): obj is IConfirmEmailCommand {
    return (
        obj !== null &&
        typeof obj === "object" &&
        typeof obj.email === "string" &&
        typeof obj.confirmToken === "string"
      );
}
