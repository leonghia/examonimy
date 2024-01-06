const millisecond = 1;
const second = millisecond * 1000;
const minute = second * 60;
const hour = minute * 60;
const day = hour * 24;
const week = day * 7;
const year = day * 365.2425;

export const convertToAgo = (datetime = new Date()) => {
    const ts = new Date().getTime() - datetime.getTime();
    if (ts / second < 10)
        return "vừa mới đây";
    if (ts / second < 60)
        return `vài giây trước`;
    if (ts / minute < 60)
        return `${Math.floor(ts / minute)} phút trước`;
    if (ts / hour < 24)
        return `${Math.floor(ts / hour)} giờ trước`;
    if (ts / day < 7)
        return `${Math.floor(ts / day)} ngày trước`;
    if (ts / day < 365)
        return `${Math.floor(ts / week)} tuần trước`;
    return `${Math.floor(ts / year)} năm trước`;
};

export const DateTimeLocaleStringStyle = {
    timeStyle: "short",
    dateStyle: "short"
};

export const constructExamSchedule = (hourValue, minuteValue, dateValue) => {
    const fromDateArr = dateValue.split("/");
    const fromDateTime = new Date(+fromDateArr[2], +fromDateArr[1] - 1, +fromDateArr[0], +hourValue, +minuteValue);
    return fromDateTime.toISOString();
}