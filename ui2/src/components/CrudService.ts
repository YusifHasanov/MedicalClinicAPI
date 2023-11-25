
 
import toast from "./Toast";


class CrudService {


    private static readonly INSTANCE = new CrudService();


    public async update(callbck: () => void) {
        if (window.confirm("Dəyişiklikləri yadda saxlamaq istədiyinizə əminsiniz?")) {
            this.handleResponse(callbck);
            return;
        }
        toast.info("Dəyişikliklər yadda saxlanılmadı");
    }
    public async delete(callbck: () => void) {
        if (window.confirm("Silmək istədiyinizə əminsiniz?")) {
            this.handleResponse(callbck);
            return;
        }
        toast.info("Silinmədi");
    }

    public async add(callbck: () => void) {
        if (window.confirm("Əlavə etmək istədiyinizə əminsiniz?")) {
            this.handleResponse(callbck);
            return;
        }
        toast.info("Əlavə edilmədi");
    }
 

    public static getInstance(): CrudService {
        return this.INSTANCE;
    }

    private async handleResponse(callbck: () => void) {
        try {
            callbck();
            toast.success("Dəyişikliklər uğurla yadda saxlandı");
        } catch (error) {
            toast.error("Dəyişikliklər yadda saxlanılarkən xəta baş verdi");
        }
    }
}
const crudService = CrudService.getInstance();
export default crudService;