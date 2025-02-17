const emailRegex = /^(?=.{1,64}@.{1,255}$)([a-zA-Z0-9._%+-]+)@([a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$/;

function isValidEmail(email: string): boolean {
  return emailRegex.test(email);
}

export {isValidEmail};
