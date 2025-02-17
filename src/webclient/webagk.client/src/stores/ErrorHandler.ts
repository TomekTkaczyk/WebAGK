import { ApiError } from "@/infrastructure/errors/ApiError";
import router from "@/router";
import { isAxiosError } from "axios";

const ErrorHandler = (error: any) => {
  if(isAxiosError(error)){
    switch(error.code) {
      case "ERR_NETWORK": {
        router.push("/Error503");
        break;
      }
      case "ERR_BAD_REQUEST": {
        throw error;
      }
      default: {
        throw error;
      }
    }
  } else {
    throw new ApiError('Wystąpił nieoczekiwany błąd.', "ApiError");
  }
}

export default ErrorHandler;
