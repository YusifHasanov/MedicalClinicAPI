import { ToastOptions, toast } from 'react-toastify';

type positionType = "top-right" | "top-center" | "top-left" | "bottom-right" | "bottom-center" | "bottom-left";

interface ToastProps {
    message: string;
    position?: "top-right" | "top-center" | "top-left" | "bottom-right" | "bottom-center" | "bottom-left";
    autoClose?: number;
    hideProgressBar?: boolean;
    closeOnClick?: boolean;
    pauseOnHover?: boolean;
    draggable?: boolean;
    progress?: undefined;
    theme?: "light" | "dark";

}


class Toast {
    
    private static toast =new Toast();
    private position: positionType = "top-right";
    private autoClose: number = 800;
    private hideProgressBar: boolean = false;
    private closeOnClick: boolean = true;
    private pauseOnHover: boolean = false;
    private draggable: boolean = true;
    private progress: string | number | undefined = undefined;
    private theme: "light" | "dark" = "light";

    private constructor() { 
    }

    public static getInstance() {
        return this.toast;
    }

    public success(message: string): void {
        toast.success(message, {
            position: this.position,
            autoClose: this.autoClose,
            hideProgressBar: this.hideProgressBar,
            closeOnClick: this.closeOnClick,
            pauseOnHover: this.pauseOnHover,
            draggable: this.draggable,
            progress: this.progress,
            theme: this.theme,
        });

    }
    public error(message: string): void {
        toast.error(message, {
            position: this.position,
            autoClose: this.autoClose,
            hideProgressBar: this.hideProgressBar,
            closeOnClick: this.closeOnClick,
            pauseOnHover: this.pauseOnHover,
            draggable: this.draggable,
            progress: this.progress,
            theme: this.theme,
        });
    }
    public warning(message: string): void {
        toast.warning(message, {
            position: this.position,
            autoClose: this.autoClose,
            hideProgressBar: this.hideProgressBar,
            closeOnClick: this.closeOnClick,
            pauseOnHover: this.pauseOnHover,
            draggable: this.draggable,
            progress: this.progress,
            theme: this.theme,
        });
    }
    public info(message: string) {
        toast.info(message, {
            position: this.position,
            autoClose: this.autoClose,
            hideProgressBar: this.hideProgressBar,
            closeOnClick: this.closeOnClick,
            pauseOnHover: this.pauseOnHover,
            draggable: this.draggable,
            progress: this.progress,
            theme: this.theme,
        });
    }


    public setPosition(position: positionType) {
        this.position = position
    }
    public setAutoClose(autoClose: number) {
        this.autoClose = autoClose;
    }
    public setHideProgressBar(hideProgressBar: boolean) {
        this.hideProgressBar = hideProgressBar;
    }
    public setPauseOnHover(pauseOnHover: boolean) {
        this.pauseOnHover = pauseOnHover;
    }
    public setDraggable(draggable: boolean) {
        this.draggable = draggable;
    }
    public setCloseOnClick(closeOnClick: boolean) {
        this.closeOnClick = closeOnClick;
    }
    public setProgress(progress: string | number) {
        this.progress = progress;
    }
    public setTheme(theme: "light" | "dark") {
        this.theme = theme;
    }
    public getPosition() {
        return this.position;
    }
    public getAutoClose() {
        return this.autoClose;
    }
    public getHideProgressBar() {
        return this.hideProgressBar;
    }
    public getPauseOnHover() {
        return this.pauseOnHover;
    }
    public getDraggable() {
        return this.draggable;
    }
    public getCloseOnClick() {
        return this.closeOnClick;
    }
    public getProgress() {
        return this.progress;
    }
    public getTheme() {
        return this.theme;
    }
}
const medicalToast = Toast.getInstance();
export default medicalToast;




