export class Dictionary<TValue> {
  private dictionary: { [key: string]: TValue } = {};

  public Exist(key: string): boolean {
    return key in this.dictionary;
  }

  public Get(key: string): TValue | undefined {
    return this.dictionary[key];
  }

  public Add(key: string, value: TValue): void {
    if (this.Exist(key)) {
      return;
    }
    this.dictionary[key] = value;
  }

  public Remove(key: string): void {
    if (!this.Exist(key)) {
      return;
    }
    delete this.dictionary[key];
  }

  public Set(key: string, value: TValue) {
    this.dictionary[key] = value;
  }

  public Clear(): void {
    this.dictionary = {};
  }

  public GetDictionary() {
    return this.dictionary;
  }

  public get Count(): number {
    return Object.keys(this.dictionary).length;
  }
}
