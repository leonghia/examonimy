export class BaseComponent {

    subscribe(eventName, handler) {
        this._events[eventName].push(handler);
    }

    _trigger(eventName, data = null) {
        for (const handler of this._events[eventName]) {
            handler(data);
        }
    }
}