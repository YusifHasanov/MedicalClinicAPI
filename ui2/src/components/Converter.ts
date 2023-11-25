

export default class Converter {


  public static  async convertToBase64(file: File): Promise<string | null> {
        return new Promise((resolve) => {
            const reader = new FileReader();
            reader.onload = (e) => resolve(e.target?.result as string);
            reader.readAsDataURL(file);
        });
    };
}