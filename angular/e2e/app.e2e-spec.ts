import { ResafeTemplatePage } from './app.po';

describe('abp-project-name-template App', function() {
  let page: ResafeTemplatePage;

  beforeEach(() => {
    page = new ResafeTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
