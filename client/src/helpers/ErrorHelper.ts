
export class ErrorHelper {
    public static handleResponseCode = (status: number, text: string | undefined, setMsgCallback: (text: string) => void) => {
        console.info(`Settings error message for \'${status}\'...`);
        if (status >= 200 && status <= 299) {
            setMsgCallback(`Выполнено. Сервер вернул код ответа HTTP_${status}.`);
        } else {
            let errorMsg = "Ошибка.";
            if (status === 0) {
                errorMsg += " Сервер недоступен.";
            } else if (status === 400) {
                errorMsg += ` Проверьте введенные данные.`;
            } else {
                errorMsg += ` Сервер вернул код ответа HTTP_${status}.`;
            }

            if (text) {
                errorMsg += ` ${text}.`;
            }

            setMsgCallback(errorMsg);
        }
    }

    public static handleOnRequest(setMsgCallback: (text: string) => void, btnsSetStateCallback: (state:boolean) => void){
        setMsgCallback("Отправка запроса...");
        console.info("Sending request...");
        btnsSetStateCallback(true);
        setTimeout(() => btnsSetStateCallback(false), 10000);   
    }
}