export default interface IMessageProvider {
  GetMessage(error: {code: string, message: string}): string;
}
