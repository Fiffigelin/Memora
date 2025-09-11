const emailRegex = new RegExp(
  /^(("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+\/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))((\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,2}[a-z0-9]))$/
);

export const isEmail = (input: string | undefined): boolean =>
  !!input && emailRegex.test(input);