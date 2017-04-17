interface Twemoji {

    parse(element: HTMLElement); void;

}

declare var twemoji: Twemoji;

declare module 'twemoji' {
    export = twemoji;
}
