﻿// <copyright file="Section.cs" company="Engage Software">
// Engage: ContactUs - http://www.engagesoftware.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Survey.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Util;

    /// <summary>
    /// Section class of a Survey
    /// </summary>
    public partial class Section : ISection
    {
        /// <summary>
        /// Gets the formatting for the element plus the the unformatted text together. Used primarily by
        /// the Web and Windows viewers only.
        /// </summary>
        /// <value></value>
        public string FormattedText
        {
            get { return this.ShowText ? (this.Formatting + this.UnformattedText) : string.Empty; }
        }

        /// <summary>
        /// Gets or sets only the text value of Text attribute for the survey element.
        /// </summary>
        /// <value></value>
        public string UnformattedText
        {
            get { return this.Text; }
        }
        
        /// <summary>
        /// Gets the formatting that will be used to prefix the unformatted text for the survey element.
        /// </summary>
        /// <value></value>
        public string Formatting
        {
            get { return string.Empty; }
        }

        #region ISection signatures

        /// <summary>
        /// Gets the question.
        /// </summary>
        /// <param name="key">The key name.</param>
        /// <returns>An IQuestion using the passed key.</returns>
        public IQuestion GetQuestion(string key)
        {
            foreach (IQuestion question in this.Questions)
            {
                if (question.RelationshipKey == key)
                {
                    return question;
                }
            }
            return null;

        }

        public List<IQuestion> GetQuestions()
        {
            List<IQuestion> questions = new List<IQuestion>();

            foreach (Question q in Questions)
            {
                questions.Add(q);
            }

            return questions;
        }

        /// <summary>
        /// Renders the specified ph.
        /// </summary>
        /// <param name="ph">The placeholder container.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <param name="showRequiredNotation">if set to <c>true</c> [show required notation].</param>
        /// <param name="validationProvider">The validation provider.</param>
        public virtual void Render(PlaceHolder ph, bool readOnly, bool showRequiredNotation, ValidationProviderBase validationProvider)
        {
            HtmlGenericControl sectionDiv = new HtmlGenericControl("DIV");
            sectionDiv.Attributes["class"] = Engage.Survey.Util.Utility.CssClassSectionWrap + " section" + this.SectionId;
            ph.Controls.Add(sectionDiv);

            // row for the section text
            HtmlGenericControl title = new HtmlGenericControl("h3");
            title.Attributes["class"] = Engage.Survey.Util.Utility.CssClassSectionTitle;
            title.InnerText = this.FormattedText;
            sectionDiv.Controls.Add(title);

            foreach (IQuestion question in this.Questions)
            {
                // create the question wrap div.
                HtmlGenericControl questionWrapDiv = new HtmlGenericControl("DIV");
                questionWrapDiv.Attributes["class"] = "qw" + question.QuestionId;
                sectionDiv.Controls.Add(questionWrapDiv);

                // create the question span with label in it for question text.
                HtmlGenericControl questionSpan = new HtmlGenericControl("SPAN");
                questionSpan.Attributes["class"] = Engage.Survey.Util.Utility.CssClassQuestion;
                questionSpan.InnerHtml = question.FormattedText;
                questionWrapDiv.Controls.Add(questionSpan);
                
                // <span class="questin">Phone<span>*</span></span>
                //// if the question is required, then add the optional * notation.
                if (question.IsRequired && showRequiredNotation)
                {
                    HtmlGenericControl requiredSpan = new HtmlGenericControl("SPAN");
                    requiredSpan.Attributes["class"] = Engage.Survey.Util.Utility.CssClassRequired;
                    requiredSpan.InnerText = "*";
                    questionSpan.Controls.Add(requiredSpan);
                }

                //// Create a span to put answer(s) in.
                Control control = Engage.Survey.Util.Utility.CreateWebControl(question, readOnly);
                questionWrapDiv.Controls.Add(control);

                if (string.IsNullOrEmpty(control.ID) == false)
                {
                    if (question.IsRequired)
                    {
                        validationProvider.RegisterValidator(ph.Page.ClientScript, ValidationType.RequiredField, "error-message", questionWrapDiv, control.ID, question.UnformattedText + " is required.", "survey", 1);
                    }

                    //DefaultObjectAttribute attribute = question.GetObjectAttribute() as DefaultObjectAttribute;
                    //if (attribute != null)
                    //{
                    //    validationProvider.RegisterValidator(ph.Page.ClientScript, ValidationType.LimitedLengthField, "error-message", questionWrapDiv, control.ID, "Max characters exceeded for [" + question.UnformattedText + "]", "survey", 1);
                    //}

                    //EmailAttribute email = question.GetObjectAttribute() as EmailAttribute;
                    //if (email != null)
                    //{
                    //    validationProvider.RegisterValidator(ph.Page.ClientScript, ValidationType.EmailField, "error-message", questionWrapDiv, control.ID, "Email Address should be in name@domain.com format.", "survey", 1);
                    //}

                    //if (question is CompositeQuestion)
                    //{
                    //    CompositeQuestion cq = (CompositeQuestion)question;
                    //    validationProvider.RegisterValidator(ph.Page.ClientScript, ValidationType.LimitedSelection, "error-message", questionWrapDiv, control.ID, string.Empty, "survey", cq.SelectionLimit);
                    //}
                }
            }
        }

        /// <summary>
        /// Posts the save processing.
        /// </summary>
        /// <param name="control">The control.</param>
        public virtual void PostSaveProcessing(WebControl control)
        {
            // no behavior, subclasses must define, if required
        }

        /// <summary>
        /// Pres the save processing.
        /// </summary>
        /// <param name="control">The control.</param>
        public virtual void PreSaveProcessing(WebControl control)
        {
            // no behavior, subclasses must define, if required
        }

        #endregion

        /// <summary>
        /// Determines whether [has multiple choice questions].
        /// </summary>
        /// <returns>
        /// <c>true</c> if [has multiple choice questions]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasMultipleChoiceQuestions()
        {
            foreach (Question question in this.Questions)
            {
                if (question.GetAnswerChoices().Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// RelativeOrderComparer class
        /// </summary>
        internal class RelativeOrderComparer : IComparer
        {
            /// <summary>
            /// ASC or DESC
            /// </summary>
            private readonly bool descending = true;

            /// <summary>
            /// Initializes a new instance of the <see cref="RelativeOrderComparer"/> class.
            /// </summary>
            public RelativeOrderComparer()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="RelativeOrderComparer"/> class.
            /// </summary>
            /// <param name="descending">if set to <c>true</c> [descending].</param>
            public RelativeOrderComparer(bool descending)
            {
                this.descending = descending;
            }

            #region IComparer Members

            /// <summary>
            /// Compares the specified o1.
            /// </summary>
            /// <param name="o1">The object1 to compare.</param>
            /// <param name="o2">The object2 to compare.</param>
            /// <returns>true or false</returns>
            public int Compare(object o1, object o2)
            {
                if (o1 == null && o2 == null)
                {
                    return 0;
                }

                ISection s1 = o1 as ISection;
                ISection s2 = o2 as ISection;

                if (s1 == null)
                {
                    throw new ArgumentException("oa1 is not an instance of Section");
                }

                if (s2 == null)
                {
                    throw new ArgumentException("oa2 is not an instance of Section");
                }

                if (this.descending)
                {
                    return s1.RelativeOrder.CompareTo(s2.RelativeOrder);
                }

                return s2.RelativeOrder.CompareTo(s1.RelativeOrder);
            }

            #endregion
        }
    }
}