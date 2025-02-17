import type IMessageProvider from '@/types/IMessageProvider';
import Error from "@/modules/errors/Error.vue";

class MessageProvider implements IMessageProvider {

  private messageDictionary: { [code: string]: string } = {};
  private readonly messageGroup: string;

  messageModules: { [key: string]: () => Promise<{ default: { [code: string]: string } }> } = {
    SignIn: () => import('@/infrastructure/messages/signInMessages'),
    SignUp: () => import('@/infrastructure/messages/signUpMessages'),
    GetUser: () => import('@/infrastructure/messages/getUserMessages'),
    RemindPassword: () => import('@/infrastructure/messages/remindPasswordMessages'),
    Logout: () => import('@/infrastructure/messages/logoutMessages'),
    UpdateProfile: () => import('@/infrastructure/messages/updateProfileMessages'),
    ChangeEmail: () => import('@/infrastructure/messages/changeEmailMessages'),
    ChangePassword: () => import('@/infrastructure/messages/changePasswordMessages'),
  };

  constructor(messageGroup: string) {
    this.messageGroup = messageGroup;
  }

  GetMessage(error: {code: string, message: string}): string {
    return this.messageDictionary[error.code] ?? error.message;
  }

  async Initialize(): Promise<void> {
    const loader = this.messageModules[this.messageGroup];
    if (!loader) {
      throw new Error(`Message group "${this.messageGroup}" not found`);
    }

    try {
      var messages = await loader();
      this.messageDictionary = messages.default;
    } catch (error) {
      console.error("Error: ",error);
      // error(`Error loading message group "@/infrastructure/${this.messageGroup}Messages":`, error.message);
    }
  }
}

export default MessageProvider;
