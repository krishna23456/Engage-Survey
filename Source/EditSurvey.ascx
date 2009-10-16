<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditSurvey.ascx.cs" Inherits="Engage.Dnn.Survey.EditSurvey" %>
<%@ Import Namespace="Engage.Survey.Util"%>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>
<div id="engage-evaluation">
    <fieldset class="ee-create-new">
        <legend class="Head"><%=Localize("CreateNewLabel.Text")%></legend>
        <div class="ee-title">
            <span class="ee-label eval-title"><%=Localize("EvalTitleLabel.Text")%></span>
            <span class="ee-input"><input class="NormalTextBox ee-required" id="EvalTitleInput" minlength="1" maxlength="256" /></span>
        </div>
        <div class="ee-description">
            <span class="ee-label eval-description"><%=Localize("EvalDescriptionLabel.Text")%></span>
            <span class="ee-input"><textarea id="EvalDescTextArea" class="NormalTextBox" maxlength="256"></textarea></span>
        </div>
        <ul class="ee-action-btns">
            <li class="primary-btn"><a href="#" title="Create New" id="EvalNew" class="create-new"><%=Localize("CreateNewEvalHyperLink.Text")%></a></li>
            <li class="primary-btn" style="display:none;"><a href="#" title="Edit" id="EvalEdit"><%=Localize("EditEvalHyperLink.Text")%></a></li>
            <li class="primary-btn" style="display:none;"><a href="#" title="Update" id="EvalUpdate"><%=Localize("UpdateEvalHyperLink.Text")%></a></li>
            <li class="secondary-btn" style="display:none;"><a href="#" title="Cancel" id="EvalCancel"><%=Localize("CancelHyperLink.Text")%></a></li>
        </ul>
    </fieldset>
    <fieldset class="ee-create-questions" id="CreateQuestions">
        <legend class="Head"><%=Localize("CreateNewQuestionsLabel.Text")%></legend>
        <div class="ee-question">
            <span class="ee-label"><%=Localize("TypeQuestionLabel.Text")%></span>
            <span class="ee-input"><textarea id="QuestionText" class="NormalTextBox ee-required" minlength="1" maxlength="256"></textarea></span>
        </div>
        <div class="ee-define-answer">
            <span class="ee-label"><%=Localize("DefineAnswerLabel.Text")%></span>
            <div class="define-answer">
                <span class="ee-input">
                    <select class="NormalTextBox answer-options" name="DefineAnswerType" id="DefineAnswerType">
                        <option value='<%=(int)ControlType.None %>'><%=Localize("SelectAnswerTypeOption.Text")%></option>
                        <option value='<%=(int)ControlType.SmallTextInputField %>'><%=Localize("ShortAnswerOption.Text")%></option>
                        <option value='<%=(int)ControlType.LargeTextInputField %>'><%=Localize("LongAnswerOption.Text")%></option>
                        <option value='<%=(int)ControlType.DropDownChoices %>'><%=Localize("SingleAnwserDropdownListOption.Text")%></option>
                        <option value='<%=(int)ControlType.VerticalOptionButtons %>'><%=Localize("SingleAnswerRadioButtonOption.Text")%></option>
                        <option value='<%=(int)ControlType.Checkbox %>'><%=Localize("MultipleAnswerCheckboxesOption.Text")%></option>
                    </select>
                </span>
                    <div id="ShortTextAnswer" style="display:none;" class="ee-answer-inputs">
                        <p>Here's a little preview of what a short answer input looks like:</p>
                        <img src="/DesktopModules/EngageSurvey/images/short-answer-input.gif" title="Short answer input" />
                    </div>
                    <div id="LongTextAnswer" style="display:none;" class="ee-answer-inputs">
                        <p>Here's a little preview of what a long answer input looks like:</p>
                        <img src="/DesktopModules/EngageSurvey/images/long-answer-input.gif" title="Long answer input" />
                    </div>
                <div class="ee-answer-inputs" id="MultipleAnswer" style="display:none;">
                    <ul class="answer-inputs">
                        <li>
                            <span class="ai-label"><%=Localize("AnswerTitle.Text")%> <span class="answer-num">1</span></span>
                            <div class="ai-selected">
                                <span class="ai-input"><input class="NormalTextBox" type="text" /></span>
                                <a href="#" title="Remove this answer, are you sure?" class="ee-delete"><%=Localize("RemoveAnswerHyperLink.Text")%></a>
                            </div>
                        </li>
                        <li>                        
                            <span class="ai-label"><%=Localize("AnswerTitle.Text")%> <span class="answer-num">2</span></span>
                            <div class="ai-selected">
                                <span class="ai-input"><input class="NormalTextBox" type="text"/></span>
                                <a href="#" title="Remove this answer, are you sure?" class="ee-delete"><%=Localize("RemoveAnswerHyperLink.Text")%></a>
                            </div>
                        </li>
                    </ul>
                    <ul class="ee-action-btns">
                        <li class="primary-btn" style="display:none;"><a href="#" title="Add New" class="add-new" id="AddNewQuestion"><%=Localize("AddNewAnswerHyperLink.Text")%></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <ul class="ee-action-btns">
            <li class="primary-btn disabled"><a href="#" title="Save and Create New" class="save-create-new" id="SaveQuestion"><%=Localize("SaveAndCreateNewQuestionHyperLink.Text")%></a></li>
            <li class="secondary-btn"><a href="#" title="Back" class="back"><%=Localize("BackHyperLink.Text")%></a></li>
        </ul>
    </fieldset>
    <fieldset id="PreviewArea" class="ee-preview-area">
        <legend class="Head"><%=Localize("PreviewAreaLabel.Text")%></legend>
        <hr />
        <ul id="ee-previews">
            <li class="ee-preview">
                <ul class="ee-pr-action-links">
                    <li><a href="#" title="Edit this question" class="ee-edit"><%=Localize("EditQuestionHyperLink.Text")%></a></li>
                    <li><a href="#" title="Copy this question and create new" class="ee-save"><%=Localize("CopyQuestionHyperLink.Text")%></a></li>
                    <li><a href="#" title="Delete this question, are you sure?" class="ee-delete"><%=Localize("DeleteAnswerHyperLink.Text")%></a></li>
                </ul>
                <span class="ee-label pv-question-label"><%=Localize("QuestionTitle.Text")%></span>
                <span class="pv-question"></span>
                <div>
                    <span class="ee-label pv-answer-label"><%=Localize("AnswerTitle.Text")%></span>
                    <span class="pv-answer"></span>
                </div>
            </li>
        </ul>
    </fieldset>   
</div>

<% if (false) { %><script type="text/ecmascript" src="JavaScript/jquery-1.3.2.debug-vsdoc.js"></script><% } %>
<script type="text/javascript">
var CurrentContextInfo = {
    WebMethodUrl: '<%= ResolveUrl("~/DesktopModules/EngageSurvey/Services.asmx") %>/',
    UserId: <%=UserId %>,
    Survey: <%=SerializedSurvey %>
};
</script>