import { isAxiosError } from "axios";
import { Dictionary } from "./Dictionary";
import { isApiError } from "./IApiError";
import MessageProvider from "@/infrastructure/messageProvider";
import { reactive, ref } from "vue";

class FormErrors {
  public readonly _dictionary = reactive(new Dictionary<string[]>());
  public readonly messages = ref<string[]>([]);

  public Add(field: string, message: string): void {
    const item = this._dictionary.Get(field);
    if (item !== undefined) {
      this._dictionary.Set(field, [...item, message]);
    } else {
      this._dictionary.Set(field, [message]);
    }
  }

  public ClearAll(): void {
    this._dictionary.Clear();
    this.messages.value.length = 0;
  }

  public Clear(field: string): void {
    if (this._dictionary.Get(field) !== undefined) {
      this._dictionary.Remove(field);
    }
  }

  public Get(field: string): string[] {
    const items = this._dictionary.Get(field);
    if (items !== undefined) {
      return items;
    }
    return [];
  }

  public get Count(): number {
    return this.messages.value.length + this._dictionary.Count;
  }

  public async CatchApiError(formName: string, error: any) {
    if(isAxiosError(error)){
      const messageProvider = new MessageProvider(formName);
      await messageProvider.Initialize();
      this.ClearAll();
      const data = error.response?.data;
      if(isApiError(data)) {
        data.validationErrors.forEach((value: { field?: any; code?: any; message?: any; }) => {
          const {code, message} = value;
          this.Add(value.field, messageProvider.GetMessage({code, message}));
        });
        if(data.code){
          const message = messageProvider.GetMessage({code: data.code, message: data.message});
          this.messages.value.push(message);
        }
      } else {
        this.messages.value.push("Nierozpoznana zawartość błędu WebApi.");
      }
    } else {
      this.messages.value.push("Nierozpoznany błąd systemowy.");
    }
  }
}

export { FormErrors };
