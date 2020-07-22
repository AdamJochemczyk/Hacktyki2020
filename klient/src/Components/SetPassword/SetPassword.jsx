import React from "react";
import { Form, Label, Input } from "reactstrap";

export default function SetPassword(props) {
  return (
    <Form>
      <Label>Set password</Label>
      <Input type="password" name="password" />
      <Label>Confirm password</Label>
      <Input type="password" name="password" />
    </Form>
  );
}
